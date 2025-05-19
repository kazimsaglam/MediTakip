
import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

void main() {
  runApp(const BarkodApp());
}

class BarkodApp extends StatelessWidget {
  const BarkodApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'MediTakip Barkod',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(useMaterial3: true),
      home: const BarkodScannerPage(),
    );
  }
}

class BarkodScannerPage extends StatefulWidget {
  const BarkodScannerPage({super.key});

  @override
  State<BarkodScannerPage> createState() => _BarkodScannerPageState();
}

class _BarkodScannerPageState extends State<BarkodScannerPage> {
  String? scannedCode;

  Future<void> sendBarcodeToServer(String barcode) async {
    try {
      final response = await http.post(
        Uri.parse("http://202.61.227.225:5598/api/barcode/create"),
        headers: {"Content-Type": "application/json"},
        body: jsonEncode({"barcode": barcode}),
      );

      if (response.statusCode == 200) {
        showDialog(
          context: context,
          builder: (_) => AlertDialog(
            title: const Text("İşlem Başarılı"),
            content: const Text("📤 Barkod başarıyla gönderildi"),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text("Tamam"),
              ),
            ],
          ),
        );
        setState(() {
          scannedCode = null;
        });
      } else {
        showDialog(
          context: context,
          builder: (_) => AlertDialog(
            title: const Text("Hata"),
            content: Text("❌ Hata: ${response.reasonPhrase}"),
            actions: [
              TextButton(
                onPressed: () => Navigator.pop(context),
                child: const Text("Kapat"),
              ),
            ],
          ),
        );
      }
    } catch (e) {
      showDialog(
        context: context,
        builder: (_) => AlertDialog(
          title: const Text("Bağlantı Hatası"),
          content: Text("❗ Bağlantı hatası: $e"),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text("Tamam"),
            ),
          ],
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[100],
      appBar: AppBar(
        title: const Text("📷 Barkod Tarayıcı"),
        backgroundColor: Colors.blueGrey,
      ),
      body: Column(
        children: [
          Container(
            margin: const EdgeInsets.all(16),
            padding: const EdgeInsets.all(8),
            decoration: BoxDecoration(
              color: Colors.black,
              borderRadius: BorderRadius.circular(12),
              border: Border.all(color: Colors.grey.shade400),
            ),
            height: 260,
            child: MobileScanner(
              onDetect: (capture) {
                final barcode = capture.barcodes.first;
                if (barcode.rawValue != null &&
                    scannedCode != barcode.rawValue) {
                  setState(() {
                    scannedCode = barcode.rawValue!;
                  });
                }
              },
            ),
          ),

          const SizedBox(height: 12),
          Padding(
            padding: EdgeInsets.only(bottom: 8.0),
            child: Text(
              "📱 Lütfen ilacın barkodunu kameraya gösterin",
              style: TextStyle(fontSize: 14, color: Colors.black54),
            ),
          ),

          if (scannedCode != null) ...[
            Text("📦 Barkod: $scannedCode", style: const TextStyle(fontSize: 16)),
            const SizedBox(height: 12),
            ElevatedButton.icon(
              onPressed: () {
                sendBarcodeToServer(scannedCode!);
              },
              icon: const Icon(Icons.send),
              label: const Text("Gönder"),
              style: ElevatedButton.styleFrom(
                padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 12),
                backgroundColor: Colors.blueAccent,
                foregroundColor: Colors.white,
                textStyle: const TextStyle(fontSize: 16),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
            ),
          ],
        ],
      ),
    );
  }
}
