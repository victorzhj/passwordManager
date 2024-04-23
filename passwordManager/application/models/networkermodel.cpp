#include "networkermodel.h"

NetworkerModel::NetworkerModel() {
    _baseUrl = "https://localhost:8081/api/";
    _userUrl = "User";
    _passwordUrl = "Password";
}

QString NetworkerModel::login(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _userUrl + "login";
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    connect(manager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    QNetworkReply *reply = manager->post(request, QJsonDocument(loginJson(user)).toJson());
    loop.exec();
    if (reply->error() == QNetworkReply::ContentNotFoundError) {
        return "NOMATCH";
    }
    replyString = reply->readAll();
    return replyString;
}

QString NetworkerModel::registerUser(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _userUrl +  "register";
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    connect(manager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    QNetworkReply *reply = manager->post(request, QJsonDocument(registerJson(user)).toJson());
    loop.exec();
    if (reply->error() == QNetworkReply::ContentNotFoundError) {
        return "NOMATCH";
    }
    replyString = reply->readAll();
    return replyString;
}

QString NetworkerModel::getSalt(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _userUrl + "getSalt";

    // Create a QUrlQuery object and set the query parameter
    QUrlQuery query;
    query.addQueryItem("username", user.username);
    url.setQuery(query);

    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    connect(manager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);
    QNetworkRequest request(url);
    QNetworkReply *reply = manager->get(request);
    if (reply->error() == QNetworkReply::ContentNotFoundError) {
        return "NOMATCH";
    }
    replyString = reply->readAll();
    return replyString;
}

QString NetworkerModel::deleteUser(const User &user)
{

}

QString NetworkerModel::getPasswords(const User &user)
{

}

QString NetworkerModel::postPassword(const User &user, const Password &password)
{

}

QString NetworkerModel::deletePassword(const User &user, const Password &password)
{

}

void NetworkerModel::setToken(const QString &authToken)
{
    _token = authToken;
}

QJsonObject NetworkerModel::loginJson(const User &user)
{
    QJsonObject json;
    json["username"] = user.username;
    json["masterPasswordHashed"] = user.masterPasswordHashed;
    return json;
}

QJsonObject NetworkerModel::registerJson(const User &user)
{
    QJsonObject json;
    json["username"] = user.username;
    json["masterPasswordHashed"] = user.masterPasswordHashed;
    json["salt"] = user.salt;
    json["derivedKeySalt"] = user.derivedKeySalt;
    return json;
}

QJsonObject NetworkerModel::postPasswordJson(const User &user, const Password &password)
{
    QJsonObject json;
    json["userId"] = user.userId;
    json["encryptedPassword"] = password.encryptedPassword;
    json["salt"] = password.salt;
    json["site"] = password.site;
    return json;
}

QJsonObject NetworkerModel::deletePasswordJson(const Password &password)
{
    QJsonObject json;
    json["passwordId"] = password.passwordId;
    return json;
}



