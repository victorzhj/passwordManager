#ifndef PASSWORD_H
#define PASSWORD_H

#include <QString>

struct Password
{
    int passwordId;
    int userId;
    QString encryptedPassword;
    QString salt;
    QString site;
};

#endif // PASSWORD_H
