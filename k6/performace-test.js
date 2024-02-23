import http from "k6/http";
import { sleep } from "k6";

const BASE_URL = "http://localhost:8080/api/v1/Products";

export let options = {
  stages: [
    { duration: "30s", target: 20 },
    { duration: "1m", target: 20 },
    { duration: "30s", target: 0 },
    { duration: "30s", target: 60 },
  ],
};

export default function () {
  http.get(BASE_URL);
  sleep(1);
}
