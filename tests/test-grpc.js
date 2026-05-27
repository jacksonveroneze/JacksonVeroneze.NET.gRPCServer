import grpc from 'k6/net/grpc';
import {check} from 'k6';
import {factoryHeaders, getToken} from "./scenarios/util.js";
import {randomItem} from "https://jslib.k6.io/k6-utils/1.4.0/index.js";

const BASE_URL = __ENV.BASE_URL || "127.0.0.1:7000";
const GET_PROFILE_PATH = "profiles.v1.ProfileQueryService/GetProfile";

const filecontent = open("./data.json");

const client = new grpc.Client();
client.load(null, 'pagination_types.proto');
client.load(null, 'profile_types.proto');
client.load(null, 'profile_query_service.proto');

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

export default (data) => {
    client.connect(BASE_URL, {
        plaintext: false
    });
    
    const correlationId = crypto.randomUUID();

    var headers = Object.assign(data.headers, { 'X-Correlation-ID': correlationId });
    
    console.log(correlationId)
    console.log('___')

    const params = {
        metadata: headers,
    };

    const id = randomItem(data.ids);
    
    const request = {profile_id: id};

    const response = client.invoke(GET_PROFILE_PATH, request, params);
    
    console.log(response)

    check(response, {
        'status is OK': (r) => r && r.status === grpc.StatusOK,
    });

    client.close();
};