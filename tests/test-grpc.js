import grpc from 'k6/net/grpc';
import {check} from 'k6';
import {factoryHeaders, getToken} from "./scenarios/util.js";
import {randomItem} from "https://jslib.k6.io/k6-utils/1.4.0/index.js";

const BASE_URL = __ENV.BASE_URL || "localhost:8093";
const GET_PROFILE_PATH = "profiles.v1.ProfileQueryService/GetProfile";
const GET_PAGE_PROFILE_PATH = "profiles.v1.ProfileQueryService/ListProfiles";
const CONNECT_TIMEOUT = __ENV.CONNECT_TIMEOUT || "2s";
const READ_TIMEOUT = __ENV.READ_TIMEOUT || "3s";

const client = new grpc.Client();

const protoRoot = '../src/main/Api/Grpc/protos';
client.load([protoRoot], 'profiles/v1/profile_query_service.proto');

const filecontent = open("./data.json");

export const options = {
    insecureSkipTLSVerify: true,

    scenarios: {
        get_profile: {
            executor: "ramping-arrival-rate",
            startRate: 100,
            timeUnit: "1s",

            stages: [
                { duration: "10s", target: 1 },
                { duration: "30s", target: 50 },
                { duration: "30s", target: 100 },
                { duration: "30s", target: 250 },
                { duration: "30s", target: 500 },
                { duration: "60s", target: 750 },
                { duration: "30s", target: 0 },
            ],

            preAllocatedVUs: 200,
            maxVUs: 1250,
            gracefulStop: "30s",
        },
    },

    thresholds: {
        checks: ["rate>=0.99"],
        grpc_req_duration: ["p(95)<300"],
        dropped_iterations: ["count==0"],
    },
};

export function setup() {
    const token = getToken();
    const headers = factoryHeaders(token);

    const ids = JSON.parse(filecontent);

    if (!Array.isArray(ids) || ids.length === 0) {
        throw new Error("data.json must contain at least one profile id.");
    }

    return {headers, ids};
}

export default (data) => {
    if (__ITER === 0) {
        client.connect(BASE_URL, {
            plaintext: false,
            timeout: CONNECT_TIMEOUT,
        });
    }

    const metadata = {
        ...data.headers,
        "x-correlation-id": crypto.randomUUID(),
    };

    // const request = {profile_id: randomItem(data.ids)};
    //
    // const response = client.invoke(
    //     //GET_PROFILE_PATH,
    //     GET_PAGE_PROFILE_PATH,
    //     request,
    //     {
    //         metadata,
    //         timeout: READ_TIMEOUT,
    //         tags: {
    //             rpc: "GetProfile",
    //         },
    //     }
    // );
    
    const request = {pagination: {page: 1, page_size: 50}};

    const response = client.invoke(
        GET_PAGE_PROFILE_PATH,
        request,
        {
            metadata,
            timeout: READ_TIMEOUT,
            tags: {
                rpc: "GetProfile",
            },
        }
    );

    check(response, {
        "grpc status is OK": (r) => r && r.status === grpc.StatusOK,
        "response has profile": (r) => r && r.message && r.message.data,
    });
};