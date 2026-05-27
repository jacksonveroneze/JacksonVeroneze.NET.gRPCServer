import http from "k6/http";
import {check, sleep} from "k6";
import {factoryHeaders, getToken} from "./scenarios/util.js";
import {randomItem} from "https://jslib.k6.io/k6-utils/1.4.0/index.js";

const BASE_URL = __ENV.BASE_URL || "https://127.0.0.1:7000";
const READ_PATH = __ENV.READ_PATH || "/v1/profiles";
const READ_TIMEOUT = __ENV.READ_TIMEOUT || "1s";

const filecontent = open("./data.json");

export let options = {
    insecureSkipTLSVerify: true,

    stages: [
        {duration: '5s', target: 1},
        {duration: '20s', target: 25}
    ]
};

export function setup() {
    const token = getToken();
    const headers = factoryHeaders(token);

    const ids = JSON.parse(filecontent);

    return {headers, ids};
}

export default function (data) {
    const id = randomItem(data.ids);
    const url = `${BASE_URL}${READ_PATH}/${id}`;

    const res = http.get(url, {
        timeout: READ_TIMEOUT,
        headers: data.headers,
        tags: {name: "GET /v1/profiles"},
    });

    check(res, {
        "status is OK": (r) => r.status === 200,
    });
}
