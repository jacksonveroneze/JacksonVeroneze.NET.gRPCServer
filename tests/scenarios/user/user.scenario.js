import {group} from 'k6';
import {crypto} from "k6/experimental/webcrypto";
import {checker} from '../util.js';
import * as user from "./user.js";

export default (baseUrl, headers) => {
    group('Create Flow - Success', () => {
        user.createUser(baseUrl, headers);
    });

    group('Get Paged Flow', () => {
        user.getUsersPaged(baseUrl, headers);
    });

    group('Get By Id Flow - Found', () => {
        const idUser = user.createUser(baseUrl, headers);
        user.getUserById(baseUrl, headers, idUser, 200);
    });

    group('Get By Id Flow - NotFound', () => {
        user.createUser(baseUrl, headers);
        user.getUserById(baseUrl, headers, crypto.randomUUID(), 404);
    });

    group('Activate Flow', () => {
        const idUser = user.createUser(baseUrl, headers);

        const responseCallOne = user.activateUser(baseUrl, headers, idUser);
        checker(responseCallOne, 'User', 'Activated', 204)

        const responseCallTwo = user.activateUser(baseUrl, headers, idUser);
        checker(responseCallTwo, 'User', 'Already activated - BadRequest', 400);

        const responseCallNotFound = user.activateUser(baseUrl, headers, crypto.randomUUID());
        checker(responseCallNotFound, 'User', 'NotFound', 404)
    });

    group('Inactivate Flow', () => {
        const idUser = user.createUser(baseUrl, headers);

        const responseCallOne = user.inactivateUser(baseUrl, headers, idUser);
        checker(responseCallOne, 'User', 'Activated', 204)

        const responseCallTwo = user.inactivateUser(baseUrl, headers, idUser);
        checker(responseCallTwo, 'User', 'Already inactivated - BadRequest', 400)
    });

    group('Delete Flow', () => {
        const idUser = user.createUser(baseUrl, headers);

        const responseCallOne = user.deleteUser(baseUrl, headers, idUser);
        checker(responseCallOne, 'User', 'Delete', 204)

        const responseCallTwo = user.deleteUser(baseUrl, headers, idUser);
        checker(responseCallTwo, 'User', 'Already Deleted - NotFound', 404);
    });

    group('Complete Flow', () => {
        const idUser = user.createUser(baseUrl, headers);
        user.getUserById(baseUrl, headers, idUser);
        user.activateUser(baseUrl, headers, idUser);
        user.inactivateUser(baseUrl, headers, idUser);
        user.deleteUser(baseUrl, headers, idUser);
    });
}