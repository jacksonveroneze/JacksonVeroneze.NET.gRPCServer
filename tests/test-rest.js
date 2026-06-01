import http from "k6/http";
import {check} from 'k6';
import {factoryHeaders, getToken} from "./scenarios/util.js";
import {randomItem} from "https://jslib.k6.io/k6-utils/1.4.0/index.js";

const BASE_URL = __ENV.BASE_URL || "http://10.0.0.150:7000";
const READ_PATH = __ENV.READ_PATH || "/v1/profiles";
const READ_TIMEOUT = __ENV.READ_TIMEOUT || "3s";

const filecontent = open("./data.json");

export const options = {
    insecureSkipTLSVerify: true,

    scenarios: {
        get_profile: {
            executor: "ramping-arrival-rate",
            startRate: 100,
            timeUnit: "1s",

            stages: [
                { duration: "30s", target: 500 },
                { duration: "30", target: 500 },

                { duration: "30s", target: 1000 },
                { duration: "30", target: 1000 },

                { duration: "30s", target: 1500 },
                { duration: "30", target: 1500 },

                { duration: "30s", target: 2000 },
                { duration: "30", target: 2000 },

                { duration: "30s", target: 0 },
            ],

            preAllocatedVUs: 200,
            maxVUs: 1000,

            gracefulStop: "30s",
        },
    },

    thresholds: {
        checks: ["rate>=0.99"],
        http_req_failed: ["rate<=0.01"],
        http_req_duration: ["p(95)<300"],
        dropped_iterations: ["count==0"],
    },
};

export function setup() {
    const token = getToken();
    const headers = factoryHeaders(token);

    const ids = JSON.parse(filecontent);

    return {headers, ids};
}

export default function (data) {
    const headers = {
        ...data.headers,
        "x-correlation-id": crypto.randomUUID(),
    };

    const id = randomItem(data.ids);
    const url = `${BASE_URL}${READ_PATH}/${id}`;

    const res = http.get(url, {
        timeout: READ_TIMEOUT,
        headers: headers,
        tags: {name: "GET /v1/profiles"},
    });

    check(res, {
        "status is OK": (r) => r.status === 200,
    });
}
