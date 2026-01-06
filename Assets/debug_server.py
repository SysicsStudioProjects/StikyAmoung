import socket

HOST = "0.0.0.0"   # Listen on all interfaces
PORT = 56975       # Any free port

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((HOST, PORT))
server.listen(1)

print(f"[DEBUG SERVER] Listening on port {PORT}...")

conn, addr = server.accept()
print(f"[CONNECTED] {addr}")

try:
    while True:
        data = conn.recv(4096)
        if not data:
            break
        print("[MESSAGE]", data.decode("utf-8", errors="ignore"))
except KeyboardInterrupt:
    print("\n[SERVER STOPPED]")
finally:
    conn.close()
    server.close()
