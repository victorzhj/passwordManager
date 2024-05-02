#ifndef SECURIFY_H
#define SECURIFY_H

#include <QString>
#include <QRandomGenerator>
#include <QCryptographicHash>
#include <openssl/aes.h>

class Securify
{
public:
    Securify();
    QString encryptPassword(QString &password, const QString &masterPassword);
    QString decryptPassword(QString &password, const QString &masterPassword);
    void hashPassword(QString &password, const QString &salt);
    QString generateSalt();
    QString generatePassword();
};

#endif // SECURIFY_H
