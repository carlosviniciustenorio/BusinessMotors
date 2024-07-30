import http from 'k6/http';
import { check } from 'k6';

export let options = {
    vus: 1, 
    iterations: 110,
    duration:'10s'
};

export default function () {
    let res = http.get('http://localhost:5253/api/anuncios?take=10&skip=0');
    check(res, {
        'status was 200': (r) => r.status == 200,
        'status was 429': (r) => r.status == 429,
        'response time was less than 500ms': (r) => r.timings.duration < 500,
    });
}
