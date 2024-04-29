#include "mainwindow.h"

#include <QApplication>
#include "utils/networker.h"
#include "structs/user.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    User user;
    user.userId = 1;
    user.username = "string6";
    user.derivedKeySalt = "string";
    user.derivedKey = "string";
    user.masterPasswordHashed = "string";
    user.salt = "string";
    user.token = "string";
    Networker net;
    net.registerUser(user);
    w.show();

    return a.exec();
}
