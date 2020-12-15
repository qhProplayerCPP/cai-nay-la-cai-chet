#ifndef CLIENT
#define CLIENT
#define _WINSOCK_DEPRECATED_NO_WARNING
#endif
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
int64_t GetFileSize(const std::string& fileName);
int RecvBuffer(SOCKET s, char* buffer, int bufferSize, int chunkSize = 4 * 1024);
int SendBuffer(SOCKET s, const char* buffer, int bufferSize, int chunkSize = 4 * 1024);
int64_t SendFile(SOCKET s, const std::string& fileName, int chunkSize = 64 * 1024);
int64_t RecvFile(SOCKET s, const std::string& fileName, int chunkSize = 64 * 1024);







