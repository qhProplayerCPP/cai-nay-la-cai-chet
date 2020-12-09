#ifndef SERVER
#define SERVER

#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>
#include <winsock2.h>
#include <WS2tcpip.h>
#include <string>
#include <string.h>
#include <vector>
#include <sstream>
#include <thread>
#include <fstream>
#pragma comment(lib, "Ws2_32.lib")
using namespace std;

//CLIENT OBJECT
struct CLIENT {
    char username[32];
    char client_ip[256];
    SOCKET sock;
};

#endif