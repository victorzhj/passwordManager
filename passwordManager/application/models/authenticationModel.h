#ifndef AUTHENTICATIONMODEL_H
#define AUTHENTICATIONMODEL_H

#include <QString>

#include "utils/networker.h"
#include "utils/jsonparser.h"
#include "structs/user.h"

class AuthenticationModel
{
public:
    AuthenticationModel();
    QString login(const QString &username, const QString &masterPassword);
    bool registerUser(const QString &username, const QString &masterPassword);
};

#endif // AUTHENTICATIONMODEL_H
