#ifndef USER_H
#define USER_H

#include <QString>

struct User
{
    int userId;
    QString username;
    QString derivedKeySalt;
    QString derivedKey;
    QString masterPasswordHashed;
    QString salt;
    QString token;
};

#endif // USER_H
