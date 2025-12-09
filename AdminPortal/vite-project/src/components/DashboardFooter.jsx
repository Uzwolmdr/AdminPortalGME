import { useState, useEffect } from 'react';
import './DashboardFooter.css';

const API_BASE_URL = 'http://localhost:5099/api';

const DashboardFooter = () => {
  const [version, setVersion] = useState('1.0');
  const [loading, setLoading] = useState(true);

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
      } finally {
        setLoading(false);
      }
    };

    fetchVersion();
  }, []);

  return (
    <footer className="dashboard-footer">
      <div className="footer-left">
        <span className="footer-version">Version {version}</span>
      </div>
      <div className="footer-right">
        <span className="footer-copyright">© 2025 gmeremit.com | All right reserved.</span>
      </div>
    </footer>
  );
};

export default DashboardFooter;

