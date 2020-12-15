#include "client.h"
int64_t GetFileSize(const std::string& fileName) 
{
    FILE* f;
    if (fopen_s(&f, fileName.c_str(), "rb") != 0) 
    {
        return -1;
    }
    _fseeki64(f, 0, SEEK_END);
    const int64_t len = _ftelli64(f);
    fclose(f);
    return len;
}
int RecvBuffer(SOCKET s, char* buffer, int bufferSize, int chunkSize = 4 * 1024) 
{
    int i = 0;
    while (i < bufferSize) 
    {
        const int l = recv(s, &buffer[i], __min(chunkSize, bufferSize - i), 0);
        if (l < 0) { return l; } 
        i += l;
    }
    return i;
}
int SendBuffer(SOCKET s, const char* buffer, int bufferSize, int chunkSize = 4 * 1024) 
{

    int i = 0;
    while (i < bufferSize) 
    {
        const int l = send(s, &buffer[i], __min(chunkSize, bufferSize - i), 0);
        if (l < 0) { return l; } 
        i += l;
    }
    return i;
}
int64_t SendFile(SOCKET s, const std::string& fileName, int chunkSize = 64 * 1024) 
{

    const int64_t fileSize = GetFileSize(fileName);
    if (fileSize < 0) { return -1; }

    std::ifstream file(fileName, std::ifstream::binary);
    if (file.fail()) { return -1; }

    if (SendBuffer(s, reinterpret_cast<const char*>(&fileSize), sizeof(fileSize)) != sizeof(fileSize)) 
    {
        return -2;
    }

    char* buffer = new char[chunkSize];
    bool errored = false;
    int64_t i = fileSize;
    while (i != 0) {
        const int64_t ssize = __min(i, (int64_t)chunkSize);
        if (!file.read(buffer, ssize)) { errored = true; break; }
        const int l = SendBuffer(s, buffer, (int)ssize);
        if (l < 0) { errored = true; break; }
        i -= l;
    }
    delete[] buffer;

    file.close();

    return errored ? -3 : fileSize;
}
int64_t RecvFile(SOCKET s, const std::string& fileName, int chunkSize = 64 * 1024) 
{
    std::ofstream file(fileName, std::ofstream::binary);
    if (file.fail()) { return -1; }

    int64_t fileSize;
    if (RecvBuffer(s, reinterpret_cast<char*>(&fileSize),
        sizeof(fileSize)) != sizeof(fileSize)) {
        return -2;
    }

    char* buffer = new char[chunkSize];
    bool errored = false;
    int64_t i = fileSize;
    while (i != 0) {
        const int r = RecvBuffer(s, buffer, (int)__min(i, (int64_t)chunkSize));
        if ((r < 0) || !file.write(buffer, r)) { errored = true; break; }
        i -= r;
    }
    delete[] buffer;

    file.close();

    return errored ? -3 : fileSize;
}

