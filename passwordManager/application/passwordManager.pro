QT       += core gui
QT       += network

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    controllers/authenticationController.cpp \
    controllers/networkercontroller.cpp \
    controllers/passwordManagementcontroller.cpp \
    main.cpp \
    mainwindow.cpp \
    models/authenticationModel.cpp \
    models/networkermodel.cpp \
    models/passwordManagementmodel.cpp


HEADERS += \
    controllers/authenticationController.h \
    controllers/networkercontroller.h \
    controllers/passwordManagementcontroller.h \
    mainwindow.h \
    models/authenticationModel.h \
    models/networkermodel.h \
    structs/password.h \
    models/passwordManagementmodel.h \
    structs/user.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
