import axios from 'axios';
export default function FileCheck(company, version, training, filename) {
  console.log('starting up everything');
  let ApiUrl =
    'http://localhost:5000/api/DataBaseLayer/getFileCheck/?FileParameters=' +
    company +
    '|' +
    version +
    '|' +
    training +
    '|' +
    filename;
  let DefaultValue = axios.get(ApiUrl);
  console.log(DefaultValue);
  return DefaultValue;
}
