#include "server.h"

int main() {
    //khởi tạo winsock
    WSADATA some_kind_of_data;
    WSAStartup(MAKEWORD(2, 2), &some_kind_of_data);

    //tạo vector clients
    vector<CLIENT> clients;
    cout << "Waiting for clients..." << endl;

    //nhận kết nối - THREAD 01
    thread accepter([&clients]() {
        sockaddr_in listen_address;
        listen_address.sin_family = AF_INET;
        listen_address.sin_port = htons(666);
        listen_address.sin_addr.S_un.S_addr = INADDR_ANY;
        SOCKET listen_socket = socket(AF_INET, SOCK_STREAM, 0);
        bind(listen_socket, (sockaddr*)&listen_address, sizeof(listen_address));
        listen(listen_socket, SOMAXCONN);
        sockaddr_in client_address;
        int client_address_size = sizeof(client_address);
        SOCKET client_socket;
        u_long non_blocking = true;
        CLIENT new_client;
        while (true) {
            client_socket = accept(listen_socket, (sockaddr*)&client_address, &client_address_size);
            recv(client_socket, new_client.username, 32, 0);
            if (client_socket != INVALID_SOCKET) { 
                ioctlsocket(client_socket, FIONBIO, &non_blocking);
                new_client.sock = client_socket;
                inet_ntop(AF_INET, &client_address.sin_addr, new_client.client_ip, 256);
                clients.push_back(new_client);
                cout << "User " << new_client.username << " Connected!" << std::endl;
            }
        }});

    //kiểm tra ai đã ngắt kết nối - THREAD 02
    thread checker([&clients]() {
        while (true) {
            for (int i = 0; i < clients.size(); i++) {
                if (send(clients[i].sock, "", 1, 0) < 0) {
                    cout << "User " << clients[i].username << " disconnected!" << std::endl;
                    clients.erase(clients.begin() + i);
                }
            }
            this_thread::sleep_for(chrono::seconds(1));
        }});

    //nhận tin từ client - THREAD 03
    thread receiver([&clients]() {
        //chức năng login: nhận id,pass rồi kiểm tra trong database. nếu có thì gửi lại tin success, không thì fail
        //register: nhận id,pass rồi add vào database
        //toàn nhận data từ client rồi chỉnh sửa trong database
        char buffer[1024];
        while (true) {
            for (int i = 0; i < clients.size(); i++) {
                memset(buffer, 0, sizeof(buffer));
                if (recv(clients[i].sock, buffer, sizeof(buffer), 0) > 1) {
                    for (int y = 0; y < clients.size(); y++)
                        if (i != y) {
                             stringstream msg;
                            msg << clients[i].username << ": " << buffer;
                            send(clients[y].sock, msg.str().c_str(), msg.str().size(), 0);
                        }
                }
            }
        }});

    //cái này k có gì :D - MAIN THREAD
    string msg;
    while (true) {
        getline( cin, msg);
        if (msg == "stop") {
            break;
        }
    }
    WSACleanup();
    quick_exit(0);
    return 0;
}