#include "authenticationmodel.h"

AuthenticationModel::AuthenticationModel() {}

QString AuthenticationModel::login(const QString &username, const QString &masterPassword)
{
    return "test";
}

bool AuthenticationModel::registerUser(const QString &username, const QString &masterPassword)
{

    Networker net;
    User user;
    user.username = username;
    QString registeredUser = net.registerUser(user);
    return false;
}
