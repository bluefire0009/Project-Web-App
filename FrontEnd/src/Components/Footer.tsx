import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer style={{ textAlign: 'center', marginTop: '50px', fontSize: '14px' }}>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
          Contact
        </a>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
          Information
        </a>
        <a href="#" style={{ margin: '0 10px', textDecoration: 'none', color: '#007BFF' }}>
          Report bug
        </a>
      </footer>
  );
};