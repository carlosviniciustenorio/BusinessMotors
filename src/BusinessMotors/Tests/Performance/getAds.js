import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '1m', target: 20 },   // carga leve
    { duration: '3m', target: 80 },   // carga média
    { duration: '4m', target: 150 },  // carga alta
    { duration: '1m', target: 0 },    // encerra
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'],
    http_req_failed: ['rate<0.05'],
  },
};

export default function () {
  const res = http.get('http://localhost:8000/api/anuncios?take=10&skip=0');

  check(res, {
    'status is 200': (r) => r.status === 200,
    'response time < 500ms': (r) => r.timings.duration < 500,
  });

  sleep(0.5);
}