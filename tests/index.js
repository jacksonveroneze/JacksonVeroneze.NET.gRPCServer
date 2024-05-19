import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";

import user from "./scenarios/user/user.scenario.js";
import mail from "./scenarios/mail/mail.scenario.js";
import ride from "./scenarios/ride/ride.scenario.js";

export let optionsRamp = {
    stages: [
        {duration: '10s', target: 2},
        {duration: '100s', target: 10},
        {duration: '60s', target: 300},
        {duration: '30s', target: 100},
        {duration: '240s', target: 5}
    ]
};

export const optionsIterations = {
    iterations: 10,
    vus: 10
};

export const optionsDuration = {
    duration: '7200s',
    vus: 5
};

export const options = optionsIterations;

const baseUrl = 'http://localhost:7000/api/v1';
// const baseUrl = 'http://localhost:8080';
// const baseUrl = 'http://localhost:8085';

export default () => {
    let headers = {
        headers: {
            'Content-Type': 'application/json',
            'X-TenantId': crypto.randomUUID(),
            'X-Correlation-ID': crypto.randomUUID()
        }
    };

    group('Endpoint User', () => {
        user(baseUrl, headers);
    });

    group('Endpoint Mail', () => {
        mail(baseUrl, headers);
    });

    group('Endpoint Ride', () => {
        ride(baseUrl, headers);
    });
}
