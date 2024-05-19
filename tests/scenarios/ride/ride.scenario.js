import {group} from 'k6';
import * as user from "../user/user.js";
import * as ride from "./ride.js";
import * as position from "../position/position.js";

export default (baseUrl, headers) => {
    group('Cancel Flow - Complete', () => {
        const idUser = user.createUser(baseUrl, headers);
        const idRide = ride.createRider(baseUrl, headers, idUser);
        ride.getById(baseUrl, headers, idRide);
        ride.accept(baseUrl, headers, idRide, idUser);
        ride.start(baseUrl, headers, idRide);
        position.createBatchPositions(baseUrl, headers, idRide, 55);
        ride.cancel(baseUrl, headers, idRide);
    });

    group('Cancel Flow - Request and Cancel', () => {
        const idUser = user.createUser(baseUrl, headers);
        const idRide = ride.createRider(baseUrl, headers, idUser);
        ride.getById(baseUrl, headers, idRide);
        ride.cancel(baseUrl, headers, idRide);
    });

    group('Cancel Flow - Accept and Cancel', () => {
        const idUser = user.createUser(baseUrl, headers);
        const idRide = ride.createRider(baseUrl, headers, idUser);
        ride.getById(baseUrl, headers, idRide);
        ride.accept(baseUrl, headers, idRide, idUser);
        ride.cancel(baseUrl, headers, idRide);
    });

    group('Finish Flow', () => {
        const idUser = user.createUser(baseUrl, headers);
        const idRide = ride.createRider(baseUrl, headers, idUser);
        ride.getById(baseUrl, headers, idRide);
        ride.accept(baseUrl, headers, idRide, idUser);
        ride.start(baseUrl, headers, idRide);
        position.createBatchPositions(baseUrl, headers, idRide, 50);
        ride.finish(baseUrl, headers, idRide);
    });
}