#include "securify.h"

Securify::Securify() {}

QString Securify::encryptPassword(QString &password, const QString &masterPassword)
{
    AES_KEY aesKey;
    AES_set_encrypt_key(reinterpret_cast<const unsigned char*>(masterPassword.toStdString().c_str()), 128, &aesKey);
    unsigned char inBlock[AES_BLOCK_SIZE];
    unsigned char outBlock[AES_BLOCK_SIZE];
    return QString("test");
}

QString Securify::decryptPassword(QString &password, const QString &masterPassword)
{
    return QString("test");
}

void Securify::hashPassword(QString &password, const QString &salt)
{
    QString passwordSalted = password + salt;
    QByteArray passwordSaltedByteArray = passwordSalted.toUtf8();
    QByteArray hashed = QCryptographicHash::hash(passwordSaltedByteArray, QCryptographicHash::Sha256);
    password = QString(hashed);
}

QString Securify::generateSalt()
{
    quint32 value = QRandomGenerator::system()->generate();
    return QString::number(value);
}

QString Securify::generatePassword()
{
    QByteArray array(8, '\0');
    QRandomGenerator::system()->fillRange(reinterpret_cast<quint32*>(array.data()), 2);
    return QString(array.toHex());
}
