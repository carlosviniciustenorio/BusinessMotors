//k6 run getAds.js

import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 1, 
    iteraction: 20,
    duration: '10s',
};

export default function () {
    let res = http.get('http://localhost:8000/api/anuncios?take=10&skip=0');
    check(res, {
        'status was 500': (r) => r.status == 500,
        'status was 429': (r) => r.status == 429,
        'status was 422': (r) => r.status == 422,
        'status was 200': (r) => r.status == 200,
        'response time was less than 500ms': (r) => r.timings.duration < 500,
    });
    sleep(1);
}
