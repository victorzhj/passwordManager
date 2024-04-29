#ifndef NETWORKERMODEL_H
#define NETWORKERMODEL_H

#include <QString.h>
#include <QMap>
#include <QUrl>
#include <QNetworkAccessManager>
#include <QNetworkReply>
#include <QNetworkRequest>
#include <QDebug>
#include <QObject>
#include <QEventLoop>
#include <QUrlQuery>
#include <QJsonDocument>
#include <QJsonObject>
#include "structs/password.h"
#include "structs/user.h"

class NetworkerModel : public QObject
{
public:
    NetworkerModel();
    QString login(const User &user);
    QString registerUser(const User &user);
    QString getSalt(const User &user);
    QString deleteUser(const User &user);
    QString getPasswords(const User &user);
    QString postPassword(const User &user, const Password &password);
    QString deletePassword(const User &user, const Password &password);
    void setToken(const QString &authToken);

private:
    // functions
    QJsonObject loginJson(const User &user);
    QJsonObject registerJson(const User &user);
    QJsonObject postPasswordJson(const User &user, const Password &password);
    QJsonObject deletePasswordJson(const Password &password);
    // variables
    QString _baseUrl;
    QString _userUrl;
    QString _passwordUrl;
    QMap<QString, QString> _urls;
    QString _token;
    QList<QSslError> _expectedSslErrors;

};

#endif // NETWORKERMODEL_H
