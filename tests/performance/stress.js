import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    stages: [
        { duration: '5s', target: 10 },
        { duration: '10s', target: 10 },
        { duration: '5s', target: 50 },
        { duration: '10s', target: 50 },
        { duration: '5s', target: 100 },
        { duration: '10', target: 100 },
        { duration: '10s', target: 0 },
    ],
    thresholds: {
        // You can define thresholds for your test here
        http_req_duration: ['p(95)<500'], // 95% of requests should be below 500ms
    },
};

export default () => {
    http.get('http://4.231.252.47:5002/Currency/GetAll');
    sleep(1);
}