import React from 'react';

export const Footer: React.FC = () => {
  return (
    <footer className='Footer'>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF', fontSize: "14px" }}>
          Contact
        </a>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF', fontSize: "14px" }}>
          Information
        </a>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF', fontSize: "14px" }}>
          Report bug
        </a>
      </footer>
  );
};