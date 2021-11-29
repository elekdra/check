import axios from 'axios';
export default function FilterData(company, version, training) {
  console.log('filter the data');
  let ApiUrl =
    'http://localhost:5000/api/DataBaseLayer/getFilteredData/?filterParameters=' +
    company +
    '|' +
    version +
    '|' +
    training;
  let DefaultValue = axios.get(ApiUrl);
  console.log(DefaultValue);
  return DefaultValue;
}
