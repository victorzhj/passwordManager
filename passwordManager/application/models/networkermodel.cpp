#include "networkermodel.h"

NetworkerModel::NetworkerModel() {
    _baseUrl = "https://localhost:8081/api/";
    _userUrl = "User/";
    _passwordUrl = "Password";
    QList<QSslCertificate> cert = QSslCertificate::fromPath("../cert/server-certificate.pem");
    QSslError error(QSslError::SelfSignedCertificate, cert.at(0));
    _expectedSslErrors.append(error);
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
    reply->ignoreSslErrors(_expectedSslErrors);

    loop.exec();
    if (reply->error() == QNetworkReply::ContentNotFoundError) {
        return "NOMATCH";
    }

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    replyString = reply->readAll();
    delete reply;
    delete manager;
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
    reply->ignoreSslErrors(_expectedSslErrors);
    loop.exec();
    if (reply->error() == QNetworkReply::ContentNotFoundError) {
        return "NOMATCH";
    }

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }
    replyString = reply->readAll();
    delete reply;
    delete manager;
    return replyString;
}

QString NetworkerModel::getSalt(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _userUrl + "getSalt";
    QUrlQuery query;
    query.addQueryItem("username", user.username);
    url.setQuery(query);
    qDebug() << url;
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    QNetworkRequest request(url);
    QNetworkReply *reply = manager->get(request);

    // Connect the finished signal of the reply to the quit slot of the loop
    QObject::connect(reply, &QNetworkReply::finished, &loop, &QEventLoop::quit);
    reply->ignoreSslErrors(_expectedSslErrors);

    // Start the loop to wait for the finished signal
    loop.exec();

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    delete reply;
    delete manager;

    return replyString;
}

QString NetworkerModel::deleteUser(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _userUrl;
    qDebug() << url;
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    request.setRawHeader("Authorization",  user.token.toLocal8Bit());
    QNetworkReply *reply = manager->deleteResource(request);

    // Connect the finished signal of the reply to the quit slot of the loop
    QObject::connect(reply, &QNetworkReply::finished, &loop, &QEventLoop::quit);
    reply->ignoreSslErrors(_expectedSslErrors);

    // Start the loop to wait for the finished signal
    loop.exec();

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    delete reply;
    delete manager;

    return replyString;
}

QString NetworkerModel::getPasswords(const User &user)
{
    QString replyString;
    QUrl url = _baseUrl + _passwordUrl;
    qDebug() << url;
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    request.setRawHeader("Authorization",  user.token.toLocal8Bit());
    QNetworkReply *reply = manager->get(request);

    // Connect the finished signal of the reply to the quit slot of the loop
    QObject::connect(reply, &QNetworkReply::finished, &loop, &QEventLoop::quit);
    reply->ignoreSslErrors(_expectedSslErrors);

    // Start the loop to wait for the finished signal
    loop.exec();

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    delete reply;
    delete manager;

    return replyString;
}

QString NetworkerModel::postPassword(const User &user, const Password &password)
{
    QString replyString;
    QUrl url = _baseUrl + _passwordUrl;
    qDebug() << url;
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    request.setRawHeader("Authorization",  user.token.toLocal8Bit());
    QNetworkReply *reply = manager->post(request, QJsonDocument(postPasswordJson(user, password)).toJson());

    // Connect the finished signal of the reply to the quit slot of the loop
    QObject::connect(reply, &QNetworkReply::finished, &loop, &QEventLoop::quit);
    reply->ignoreSslErrors(_expectedSslErrors);

    // Start the loop to wait for the finished signal
    loop.exec();

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    delete reply;
    delete manager;

    return replyString;
}

QString NetworkerModel::deletePassword(const User &user, const Password &password)
{
    QString replyString;
    QUrl url = _baseUrl + _passwordUrl;
    QUrlQuery query;
    query.addQueryItem("passwordId", QString::number(password.passwordId));
    url.setQuery(query);
    qDebug() << url;
    QNetworkAccessManager *manager = new QNetworkAccessManager();
    QEventLoop loop;
    QNetworkRequest request(url);
    request.setHeader(QNetworkRequest::ContentTypeHeader, "application/json");
    request.setRawHeader("Authorization",  user.token.toLocal8Bit());
    QNetworkReply *reply = manager->deleteResource(request);

    // Connect the finished signal of the reply to the quit slot of the loop
    QObject::connect(reply, &QNetworkReply::finished, &loop, &QEventLoop::quit);
    reply->ignoreSslErrors(_expectedSslErrors);

    // Start the loop to wait for the finished signal
    loop.exec();

    if (reply->error() == QNetworkReply::NoError) {
        replyString = reply->readAll();
        qDebug() << replyString;
    } else {
        qDebug() << reply->errorString();
        qDebug() << "TEST";
        return "NOMATCH";
    }

    delete reply;
    delete manager;

    return replyString;
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



