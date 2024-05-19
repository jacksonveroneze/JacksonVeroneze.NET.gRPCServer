import {check} from 'k6';

export function checker(response, tag, desc, statusCode) {
    const des = `[${tag}] - ${desc} - status is ${statusCode}`;

    check(response, {[des]: (r) => r.status === statusCode});
}