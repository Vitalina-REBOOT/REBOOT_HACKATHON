import telepot
from telepot.loop import MessageLoop
import pymysql
from pymysql.cursors import DictCursor
import apiai
import json

token = '1032884247:AAFVu11PfhrlTg0JA8Xh63qs5ejk9CTwsX4'
telepot.api.set_proxy('http://82.119.170.106:8080')
TelegramBot = telepot.Bot(token)
print('started')

def dialog_flow_connector(text):
    request = apiai.ApiAI('ВАШ API ТОКЕН').text_request()  # Токен API к Dialogflow
    request.lang = 'ru'  # На каком языке будет послан запрос
    request.session_id = 'BatlabAIBot'  # ID Сессии диалога (нужно, чтобы потом учить бота)
    request.query = text  # Посылаем запрос к ИИ с сообщением от юзера
    responseJson = json.loads(request.getresponse().read().decode('utf-8'))
    response = responseJson['result']['fulfillment']['speech']  # Разбираем JSON и вытаскиваем ответ
    # Если есть ответ от бота - присылаем юзеру, если нет - бот его не понял
    if response:
        print(response)
        #bot.send_message(chat_id=update.message.chat_id, text=response)
    else:
       print("Я не понял")


connection = pymysql.connect(
    host='localhost',
    port = 3307,
    user='root',
    password='1234',
    db='hted_bot',
    charset='utf8mb4',
    cursorclass=DictCursor
)

def knock_to_base(query):
    with connection:
        try:
            cursor = connection.cursor()
            cursor.execute(query)
            return cursor
        except Exception as ex:
            print(f'error:{ex}')



def handle(msg):

    """ Process request like '3+2' """
    content_type, chat_type, chat_id = telepot.glance(msg)
    print(msg)
    text = msg["text"]
    text=text.lower()
    user_id = msg["from"]["username"]
    if(msg["text"] == '/help'):
        answer = 'Для регистрации, введите ваш логин в формате: /reg [логин]\n ' \
                 'Чтобы узнать свои предстоящие события, введите команду /events\n' \
                 'Чтобы посмотреть список своих to-do заданий, введите /tasks\n'
        TelegramBot.sendMessage(chat_id, answer)
        return

    if('/reg' in msg["text"]):
        try:
            text = msg["text"]
            words = text.split(" ")
            login=words[1]

            cursor = knock_to_base(f"CALL `hted_bot`.`fing_if_user_exists`('{login}')")
            if(cursor.rowcount>0):
                try:


                    knock_to_base(f"INSERT INTO `hted_bot`.`tlg_users` (`tlg_login`,`user_login`) VALUES ('{user_id}','{login}');")
                    TelegramBot.sendMessage(chat_id, f'Пользователь {login} добавлен c id {user_id}')
                except Exception as ex:
                    print(ex)
                    TelegramBot.sendMessage(chat_id, 'Произошла ошибка при регистрации, повторите запрос')

            else:
                TelegramBot.sendMessage(chat_id, 'Табельный номер не зарегистрирован')

        except Exception as ex:
            TelegramBot.sendMessage(chat_id, 'Произошла ошибка, повторите запрос')
            print(ex)
            return

    cursor = knock_to_base(f"CALL `hted_bot`.`fing_if_tlg_id_exists`('{user_id}')")

    if (cursor is None or cursor.rowcount == 0):
        TelegramBot.sendMessage(chat_id, 'Простите, я вас не узнаю. Необходимо пройти процедуру регистрации. ' +
                                'Для регистрации, введите ваш логин номер в формате: /reg [логин])')
        print(f'unknown_user: {user_id}')
        return

    # Блок, когда распознали юзверя
    else:
        if(text=='/events'):
            cursor = knock_to_base(f"CALL `hted_bot`.`get_user_events`('{user_id}');")
            answer = 'Список Ваших предстоящих событий:\n'
            for rec in cursor._rows:
                answer= answer + f'Событие {rec["name"]} произойдёт {rec["ev_date"]} в {rec["ev_time"]} по ссылке {rec["link"]}\n'

            TelegramBot.sendMessage(chat_id,answer)
        elif(text=='/tasks'):
            cursor = knock_to_base(f"CALL `hted_bot`.`get_user_tasks`('{user_id}');")
            answer = 'Список Ваших to-do заданий:\n'
            for rec in cursor._rows:
                answer = answer + f'{rec["Task"]}\n'

            TelegramBot.sendMessage(chat_id, answer)
        else:
            answer = dialog_flow_connector(text)
            TelegramBot.sendMessage(chat_id, answer)




        TelegramBot.sendMessage(chat_id, 'Задайте вопрос или введите команду. Введите /help для получения списка команд')
        return


def dialog_flow_connector(text):
    request = apiai.ApiAI('a27db84e7321447794fad5f02783fa16').text_request()
    request.lang = 'ru'
    request.session_id = 'Hted_bot'
    request.query = text
    responseJson = json.loads(request.getresponse().read().decode('utf-8'))
    response = responseJson['result']['fulfillment']['speech']

    if response:
        print(response)
        # Если вернул запрос, то стучимся в базу
        if ("[QUERY]" in response):
            words = response.split("теги: ")
            print(words[1])
            tag_part = words[1]
            tags = tag_part.split(";")
            db_payload = ""
            for tag in tags:
                if (len(tag) > 0):
                    db_payload = db_payload + f'+{tag}'

            print(db_payload)

            cursor = knock_to_base(f"CALL `hted_bot`.`find_docs_by_payload`('zombirable', '{db_payload}')")
            if(cursor.rowcount>0):
                answer="Вот что я нашёл по Вашему запросу\n"

                for rec in cursor._rows:
                    answer=answer+f"{rec['name']}\nссылка: {rec['link']}\n"

            else:
                answer = "Извините, но по вашему запросу ничего не нашлось"

            return answer
        # Иначе, транслируем напрямую
        else:
            return response


MessageLoop(TelegramBot, handle).run_as_thread()

# Keep the program running.
while True:
    n = input('Для остановки, введите stop\n')
    if n.strip() == 'stop':
        break