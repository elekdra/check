import React from 'react';
import AdminDashBoardButtonStyle from '../AdminDashboardButtonStyle/AdminDashBoardButtonStyle';
import user from '../../../assets/user.png';
import companymanagement from '../../../assets/companymanagement.png';
import help from '../../../assets/help.png';
import log from '../../../assets/log.png';
import testqs from '../../../assets/testqs.png';
import docmanagement from '../../../assets/docmanagement.png';
import './AdminDashboardButton.css';
function AdminDashboardButtons() {
  const data = [
    {
      link_icon: user,
      link_title: 'User Management',
      component_class: '/notfound',
    },
    {
      link_icon: docmanagement,
      link_title: 'Training Documents Management',
      component_class: '/dashboard',
    },
    {
      link_icon: companymanagement,
      link_title: 'Company Management',
      component_class: '/notfound',
    },
    {
      link_icon: testqs,
      link_title: 'Test Questions Management',
      component_class: '/notfound',
    },
    {
      link_icon: log,
      link_title: 'View Acivity Log',

      component_class: '/notfound',
    },
    {
      link_icon: help,
      link_title: 'Help',
      component_class: '/notfound',
    },
  ];
  return (
    <div className='Admin-Container'>
      {data.map((item, index) => (
        <AdminDashBoardButtonStyle key={index} item={item} />
      ))}
    </div>
  );
}

export default AdminDashboardButtons;
