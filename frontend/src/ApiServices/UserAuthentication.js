import axios from 'axios';
export default function UserAuthentication(username, password) {
  let ApiUrl =
    'http://localhost:5000/api/databaselayer/authenticate?credentials=' +
    username +
    '|' +
    password;
  let response = axios.get(ApiUrl);
  console.log(response.data);
  return response;
}
