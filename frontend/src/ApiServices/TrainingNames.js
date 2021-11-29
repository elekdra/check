import axios from 'axios';
export default function TrainingNames() {
  let ApiUrl = 'http://localhost:5000/api/databaselayer/training/';
  let DefaultValue = axios.get(ApiUrl);
  console.log(DefaultValue);
  return DefaultValue;
}
