import axios from 'axios';
export default function StartUpDefaultsValue() {
  console.log('starting up everything');
  let ApiUrl =
    'http://localhost:5000/api/task/getdefaults/?initialize=StartupInitiation';
  let DefaultValue = axios.get(ApiUrl);
  console.log(DefaultValue);
  return DefaultValue;
}
