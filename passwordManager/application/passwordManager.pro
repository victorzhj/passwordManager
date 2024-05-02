QT       += core gui
QT       += network

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    controllers/authenticationController.cpp \
    controllers/passwordManagementcontroller.cpp \
    main.cpp \
    mainwindow.cpp \
    models/authenticationModel.cpp \
    models/passwordManagementmodel.cpp \
    utils/jsonparser.cpp \
    utils/networker.cpp \
    utils/securify.cpp
    utils


HEADERS += \
    controllers/authenticationController.h \
    controllers/passwordManagementcontroller.h \
    mainwindow.h \
    models/authenticationModel.h \
    structs/password.h \
    models/passwordManagementmodel.h \
    structs/user.h \
    utils/jsonparser.h \
    utils/networker.h \
    utils/securify.h

FORMS += \
    mainwindow.ui

INCLUDEPATH += C:\Qt\Tools\OpenSSLv3\Win_x64\include
LIBS += -LC:\Qt\Tools\OpenSSLv3\Win_x64\lib -lssl -lcrypto

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
