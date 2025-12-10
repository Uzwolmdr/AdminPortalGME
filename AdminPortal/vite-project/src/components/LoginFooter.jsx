import { useState, useEffect } from 'react';
import './LoginFooter.css';

// Use environment variable with fallback:
// - In development mode: use localhost:5099
// - In production: use relative path /api (for same-domain deployment)
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 
  (import.meta.env.DEV ? 'http://localhost:5099/api' : '/api');

const LoginFooter = () => {
  const [version, setVersion] = useState('1.0');

  useEffect(() => {
    const fetchVersion = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/Login/GetVersion`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (response.ok) {
          // GetVersion returns a string directly (Ok(version) in C#)
          // Try text first since it's a string response
          const textData = await response.text();
          if (textData) {
            // Remove quotes if present and trim
            let cleanVersion = textData.trim();
            // Remove surrounding quotes if JSON serialized
            cleanVersion = cleanVersion.replace(/^["']|["']$/g, '');
            if (cleanVersion) {
              setVersion(cleanVersion);
              console.log('Version fetched:', cleanVersion);
            }
          }
        }
      } catch (error) {
        console.error('Failed to fetch version:', error);
        // Keep default version on error
      }
    };

    fetchVersion();
  }, []);

  return (
    <footer className="login-footer">
      <div className="login-footer-left">
        <span className="login-footer-version">Version {version}</span>
      </div>
      <div className="login-footer-right">
        <span className="login-footer-copyright">© 2025 gmeremit.com | All right reserved.</span>
      </div>
    </footer>
  );
};

export default LoginFooter;

