import React from 'react';

const SignUpSection: React.FC = () => {
  return (
    <div>
      {/* Container for Sign Up and Remove buttons */}
      <div style={{ display: 'flex', justifyContent: 'center', gap: '20px', marginTop: '30px' }}>
        {/* Sign Up button section */}
        <div style={{ textAlign: 'center' }}>
          <h3>Sign up</h3>
          <p>Sign up for event</p>
          <button style={{ padding: '10px 20px', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}>
            SIGN UP
          </button>
        </div>
        {/* Remove Sign Up button section */}
        <div style={{ textAlign: 'center' }}>
          <h3>Remove sign up</h3>
          <p>Remove your signup from the event</p>
          <button style={{ padding: '10px 20px', backgroundColor: '#007BFF', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer' }}>
            REMOVE
          </button>
        </div>
      </div>
      {/* Footer Section */}
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
    </div>
  );
};

export default SignUpSection;