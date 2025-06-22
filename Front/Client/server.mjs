import { createServer } from "https";
import { parse } from "url";
import next from "next";
import { readFileSync } from "fs";
import { dirname, join } from "path";
import { fileURLToPath } from "url";

const hostname = "localhost";
const port = 3000;

const __dirname = dirname(fileURLToPath(import.meta.url));

const app = next({ dev: false, hostname, port });
const handle = app.getRequestHandler();

const httpsOptions = {
  key: readFileSync(join(__dirname, "192.168.2.159+2-key.pem")),
  cert: readFileSync(join(__dirname, "192.168.2.159+2.pem")),
};

app.prepare().then(() => {
  createServer(httpsOptions, (req, res) => {
    const parsedUrl = parse(req.url, true);
    handle(req, res, parsedUrl);
  }).listen(port, hostname, () => {
    console.log(`🚀 Server running at https://${hostname}:${port}`);
  });
});
