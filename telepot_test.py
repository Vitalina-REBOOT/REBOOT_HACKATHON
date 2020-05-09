import telepot
from telepot.loop import MessageLoop
import pymysql
from pymysql.cursors import DictCursor

token = '1032884247:AAFVu11PfhrlTg0JA8Xh63qs5ejk9CTwsX4'
telepot.api.set_proxy('http://82.119.170.106:8080')
TelegramBot = telepot.Bot(token)
print('started')


dict = {"курлык":"Курлык-курлык","Какие корабли":"НЕ СТУКАЙ"}

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
    if(msg["text"] == '/help'):
        answer = 'Для регистрации, введите ваш табельный номер в формате: /reg [Табельный номер]'
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

                    user_id=msg["from"]["username"]
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

    answer = 'Здраститяъ'
    TelegramBot.sendMessage(chat_id, answer)



MessageLoop(TelegramBot, handle).run_as_thread()

# Keep the program running.
while True:
    n = input('Здраститя\n')
    if n.strip() == 'stop':
        break