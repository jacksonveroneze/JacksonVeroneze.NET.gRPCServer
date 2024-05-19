import http from 'k6/http';
import {check} from 'k6';
import {checker} from '../util.js';

import {randomIntBetween, randomItem, randomString,} from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export function createUser(baseUrl, headers) {
    const genders = ['Male', 'Female'];

    const bodyUser = JSON.stringify({
        name: `${randomString(8)} ${randomString(8)}`,
        birthday: `2023-${randomIntBetween(10, 12)}-${randomIntBetween(10, 25)}`,
        gender: `${randomItem(genders)}`,
        document: "06399214939",
        email: `${randomString(10)}@mail.com`
    });

    const responsePostUser = http.post(`${baseUrl}/users`, bodyUser, headers);
    check(responsePostUser, {'[User] - Created - status is 201': (r) => r.status === 201});

    const userContent = JSON.parse(responsePostUser.body);

    return userContent.data.id;
}

export function getUsersPaged(baseUrl, headers) {
    const response = http.get(`${baseUrl}/users`, headers);
    checker(response, 'User', 'GetPaged', 200)
}

export function getUserById(baseUrl, headers, idUser, statusCodeDefault = null) {
    const response = http.get(`${baseUrl}/users/${idUser}`, headers);

    if (statusCodeDefault) {
        checker(response, 'User', 'GetById', statusCodeDefault)
    }

    return response;
}

export function activateUser(baseUrl, headers, idUser) {
    return http.put(`${baseUrl}/users/${idUser}/activate`, {}, headers);
}

export function inactivateUser(baseUrl, headers, idUser) {
    return http.put(`${baseUrl}/users/${idUser}/inactivate`, {}, headers);
}

export function deleteUser(baseUrl, headers, idUser) {
    return http.del(`${baseUrl}/users/${idUser}`, null, headers);
}
