from telegram.ext import Updater, CommandHandler, MessageHandler, Filters


REQUEST_KWARGS={
    # "USERNAME:PASSWORD@" is optional, if you need authentication:
    'proxy_url': 'socks5://193.47.35.53:16320',
}


TOKEN='1032884247:AAFVu11PfhrlTg0JA8Xh63qs5ejk9CTwsX4'
#request_kwargs=REQUEST_KWARGS,
updater = Updater(token=TOKEN, request_kwargs=REQUEST_KWARGS, use_context=True) # Токен API к Telegram
dispatcher = updater.dispatcher

#HANDLERS
def startCommand(bot, update):
    bot.send_message(chat_id=update.message.chat_id, text='Привет, давай пообщаемся?')
def textMessage(bot, update):
    response = 'Получил Ваше сообщение: ' + update.message.text
    bot.send_message(chat_id=update.message.chat_id, text=response)

# commands
start_command_handler = CommandHandler('start', startCommand)
text_message_handler = MessageHandler(Filters.text, textMessage)


# adding commands
dispatcher.add_handler(start_command_handler)
dispatcher.add_handler(text_message_handler)

#updater starting
updater.start_polling(clean=True)

#stop_bot
updater.idle()