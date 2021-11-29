import axios from 'axios';
export default function CompanyNames() {
  console.log('enter here');
  console.log('Company names');
  let ApiUrl = 'http://localhost:5000/api/databaselayer/company/';
  let DefaultValue = axios.get(ApiUrl);
  console.log(DefaultValue);
  return DefaultValue;
}
