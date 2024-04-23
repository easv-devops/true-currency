import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    stages: [
        { duration: '10s', target: 50 },
        { duration: '30s', target: 50 },
        { duration: '10s', target: 0 }
    ],
    thresholds: {
        http_req_duration: ['p(99)<100'],
    }
};

export default () => {
    http.get('http://4.231.252.47:5002/Currency/GetAll');
    sleep(1);
}