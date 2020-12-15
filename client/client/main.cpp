#include "client.h"
int main()
{
    //khởi tạo winsock
    WSADATA some_kind_of_data;
    WSAStartup(MAKEWORD(2, 2), &some_kind_of_data);

    //tạo cái socket
    sockaddr_in connect_adress;
    connect_adress.sin_family = AF_INET;
    connect_adress.sin_port = htons(666);
    inet_pton(AF_INET, "127.0.0.1", &connect_adress.sin_addr);
    SOCKET connection_socket = socket(AF_INET, SOCK_STREAM, 0);

    //kết nối đến sv - THREAD 01
    bool is_connected = false;

    thread connector([&connection_socket, &connect_adress, &is_connected]()
        {
            char username[32];
            cout << "Enter your username: ";
            cin.get(username, 32);
            while (true) {
                if (send(connection_socket, "", 1, 0) == SOCKET_ERROR) {
                    is_connected = false;
                    connection_socket = socket(AF_INET, SOCK_STREAM, 0);
                    while (true) {
                        cout << "Trying to connect to server...\n";
                        if (connect(connection_socket, (sockaddr*)&connect_adress, sizeof(connect_adress)) != SOCKET_ERROR) {
                            send(connection_socket, username, strlen(username) + 1, 0);
                            cout << "Connected to server!\n";
                            //chức năng login: nhập id,pass rồi gửi cho sv
                            //chức năng register
                            //============nếu login rồi===========
                            //đổi mk,thông tin
                            //check user
                            //kiểm tra thằng nào online để chat 1:1
                            is_connected = true;
                            break;
                        }
                    }
                }
                this_thread::sleep_for(chrono::seconds(1));
            }});

    //nhận tin nhắn từ sv - THREAD 02
    thread receiver([&connection_socket, &is_connected]()
        {
            //download file ở thread này
            char buffer[1024];
            while (true) {
                if (is_connected == true) {
                    memset(buffer, 0, sizeof(buffer));
                    if (recv(connection_socket, buffer, sizeof(buffer), 0) > 1)
                        cout << buffer << endl;
                }
                else  this_thread::sleep_for(chrono::seconds(1));
            }});

    //gửi tin nhắn - MAIN THREAD
    //upload file ở thread này
    string msg;
    while (true) {
        if (is_connected == true) {
            getline(cin, msg);
            if (send(connection_socket, msg.c_str(), msg.size() + 1, 0) <= 0)
                cout << "Failed to send the message...\n";
        }
        else this_thread::sleep_for(chrono::seconds(1));
    }

    closesocket(connection_socket);
    WSACleanup();
    return 0;
}