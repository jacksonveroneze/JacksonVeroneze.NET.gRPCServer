import {group} from 'k6';
import * as user from "../user/user.js";
import * as mail from "./mail.js";

export default (baseUrl, headers) => {
    group('Create Flow', () => {
        const idUser = user.createUser(baseUrl, headers);
        mail.createBatchEmails(baseUrl, headers, idUser, 10);
    });

    group('Get Flow', () => {
        const idUser = user.createUser(baseUrl, headers);
        mail.createBatchEmails(baseUrl, headers, idUser, 10);
        mail.getEmailsPaged(baseUrl, headers, idUser);
    });

    group('Delete Flow', () => {
        const idUser = user.createUser(baseUrl, headers);
        mail.createBatchEmails(baseUrl, headers, idUser, 10);
        mail.deleteAllEmails(baseUrl, headers, idUser);
    });
}