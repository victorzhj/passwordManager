#include "mainwindow.h"

#include <QApplication>
#include "utils/networker.h"
#include "structs/user.h"

#include <QRandomGenerator>
#include <QString>
#include <QByteArray>


int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    MainWindow w;
    User user;
    user.userId = 1;
    user.username = "string";
    user.derivedKeySalt = "string";
    user.derivedKey = "string";
    user.masterPasswordHashed = "string";
    user.salt = "string";
    user.token = "string";
    Networker net;
    net.registerUser(user);
    w.show();
    QByteArray array(8, '\0');
    QRandomGenerator::global()->fillRange(reinterpret_cast<quint32*>(array.data()), 2);
    qDebug() << QString(array.toHex());
    return a.exec();
}
