import { useState, useEffect } from 'react';
import { getVersionAPI } from '../services/api';
import './VersionDisplay.css';

const VersionDisplay = () => {
  const [version, setVersion] = useState('1.0');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchVersion = async () => {
      try {
        const response = await getVersionAPI();
        // GetVersion returns a string directly
        if (response && typeof response === 'string') {
          // Remove any quotes and trim whitespace
          let cleanVersion = response.trim();
          cleanVersion = cleanVersion.replace(/^["']|["']$/g, '');
          if (cleanVersion) {
            setVersion(cleanVersion);
            console.log('Version fetched:', cleanVersion);
          }
        } else if (response?.Data?.version) {
          setVersion(response.Data.version);
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
    <div className="version-display">
      {loading ? (
        <span className="version-text">Version 1.0</span>
      ) : (
        <span className="version-text">Version {version}</span>
      )}
    </div>
  );
};

export default VersionDisplay;

