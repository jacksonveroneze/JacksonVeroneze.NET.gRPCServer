import http from 'k6/http';
import {checker} from '../util.js';
import {randomString} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export function createBatchEmails(baseUrl, headers, idUser, total = 5) {
    for (let i = 0; i < total; i++) {
        const bodyEmail = JSON.stringify({
            email: `${randomString(8)}@mail.com`
        });

        const response = http.post(`${baseUrl}/users/${idUser}/emails`, bodyEmail, headers);
        checker(response, 'Mail', 'Created', 201)
    }
}

export function getEmailsPaged(baseUrl, headers, idUser) {
    const response = http.get(`${baseUrl}/users/${idUser}/emails`, headers);
    checker(response, 'Mail', 'GetPaged', 200)
}

export function deleteAllEmails(baseUrl, headers, idUser) {
    const responseGetPaged = http.get(`${baseUrl}/users/${idUser}/emails`, headers);

    const result = JSON.parse(responseGetPaged.body);

    for (let email of result.data) {
        const response = http.del(`${baseUrl}/users/${idUser}/emails/${email.id}`, null, headers);
        checker(response, 'Mail', 'DeleteAll', 204)
    }
}