import http from "k6/http";

export function factoryHeaders(token) {
    return {
        'Accept': "application/json",
        'Authorization': `Bearer ${token}`
    };
}

export function getToken() {
    const url = `${__ENV.URL_TOKEN}`;
    const payload = JSON.stringify({
        client_id: `${__ENV.CLIENT_ID_TOKEN}`,
        client_secret: `${__ENV.CLIENT_SECRET_TOKEN}`,
        audience: `${__ENV.AUDIENCE_TOKEN}`,
        grant_type: `${__ENV.GRANT_TYPE_TOKEN}`,
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(url, payload, params);

    if (res.status !== 200) {
        throw new Error(`Failed to get token. status=${res.status} body=${res.body}`);
    }
    
    let token = res.json().access_token;

    // console.log(token)

    return token;
}