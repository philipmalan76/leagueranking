import logging

# Logging helper class
class LoggingHelper:

    @staticmethod
    def log_file(error):
        f = open("./error.log", "a") # Would be better to add this to a settings file as well as rotate it on a daily basis

        # writing in the file
        f.writelines(str(str(error)))

        # closing the file
        f.close()

    @staticmethod
    def log_console(error: str):
        logging.exception(error)