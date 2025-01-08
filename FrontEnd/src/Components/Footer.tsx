import React from 'react';
import { NavLink } from "react-router";

export const Footer: React.FC = () => {
  return (
    <footer className='Footer'>
      <NavLink to="/contact" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
        Contact
      </NavLink>
      <NavLink to="/contact" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
        Information
      </NavLink>
      <NavLink to="/bug" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }} end>
        Report bug
      </NavLink>
    </footer>
  );
};

export default Footer;