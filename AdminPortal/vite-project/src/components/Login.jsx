import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginAPI } from '../services/api';
import './Login.css';

const Login = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: '',
    userCode: '',
    password: '',
  });
  const [keepLoggedIn, setKeepLoggedIn] = useState(true);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [responseMessage, setResponseMessage] = useState('');
  const [isSuccess, setIsSuccess] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
    setError('');
    setResponseMessage('');
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setResponseMessage('');
    setLoading(true);

    try {
      const response = await loginAPI(formData.email, formData.userCode, formData.password);
      
      if (response) {
        console.log('Login response:', response);
        
        // Extract ResponseDescription and ResponseCode from the API response
        const responseCode = response.ResponseCode || response.responseCode;
        const responseDescription = response.ResponseDescription || response.responseDescription;
        
        if (responseDescription) {
          setResponseMessage(responseDescription);
          setIsSuccess(responseCode === '100' || responseCode === 100);
        } else {
          // If no ResponseDescription, check if login was successful
          setIsSuccess(responseCode === '100' || responseCode === 100);
          setResponseMessage(responseCode === '100' || responseCode === 100 
            ? 'Login successful!' 
            : 'Login failed. Please check your credentials.');
        }
        
        // Handle successful login (redirect, store token, etc.)
        if (responseCode === '100' || responseCode === 100) {
          // Store user info in localStorage for use in other pages
          localStorage.setItem('userEmail', formData.email);
          localStorage.setItem('userCode', formData.userCode);
          
          // Redirect to dashboard page after a short delay to show success message
          setTimeout(() => {
            navigate('/dashboard');
          }, 1000);
        }
      }
    } catch (err) {
      console.error('Login error:', err);
      
      // Check if error contains ResponseDescription from API
      if (err.data) {
        const responseCode = err.data.ResponseCode || err.data.responseCode;
        const responseDescription = err.data.ResponseDescription || err.data.responseDescription;
        
        if (responseDescription) {
          setResponseMessage(responseDescription);
          setIsSuccess(responseCode === '100' || responseCode === 100);
        } else {
          setError(err.message || 'Login failed. Please check your credentials and ensure the API server is running.');
        }
      } else {
        // Network or other errors
        setError(err.message || 'Login failed. Please check your credentials and ensure the API server is running.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      {/* Top Left - GME Logo */}
      <div className="gme-logo">
        <img src="/GME-LOGO-HD.png" alt="GME Logo" className="gme-logo-image" />
      </div>

      {/* Split Layout */}
      <div className="login-split-layout">
        {/* Left Panel - Dark with 3D Graphic */}
        <div className="login-left-panel">
          <div className="gme-3d-graphic">
            <div className="gme-layer layer-top">
              <div className="layer-content">
                <span className="layer-letter">GME</span>
                <div className="layer-dots">
                  <span className="dot"></span>
                  <span className="dot"></span>
                  <span className="dot"></span>
                </div>
              </div>
            </div>
            <div className="gme-layer layer-middle">
              <div className="layer-content">
                <span className="layer-letter orange">M</span>
                <div className="layer-dots">
                  <span className="dot orange"></span>
                  <span className="dot orange"></span>
                  <span className="dot orange"></span>
                </div>
              </div>
            </div>
            <div className="gme-layer layer-bottom">
              <div className="layer-content">
                <span className="layer-letter">E</span>
                <div className="layer-dots">
                  <span className="dot"></span>
                  <span className="dot"></span>
                  <span className="dot"></span>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Right Panel - White Login Form */}
        <div className="login-right-panel">
          <div className="login-form-container">
            <h1 className="login-title">Admin Login</h1>
            <p className="login-subtitle">Sign in to continue</p>

            {error && (
              <div className="alert alert-error" role="alert">
                {error}
              </div>
            )}

            {responseMessage && (
              <div className={`alert ${isSuccess ? 'alert-success' : 'alert-error'}`} role="alert">
                {responseMessage}
              </div>
            )}

            <form onSubmit={handleSubmit} className="login-form" autoComplete="off">
              <div className="form-group">
                <div className="input-wrapper">
                  <svg className="input-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M10 10C12.7614 10 15 7.76142 15 5C15 2.23858 12.7614 0 10 0C7.23858 0 5 2.23858 5 5C5 7.76142 7.23858 10 10 10Z" fill="#9CA3AF"/>
                    <path d="M10 12C5.58172 12 2 13.7909 2 16V20H18V16C18 13.7909 14.4183 12 10 12Z" fill="#9CA3AF"/>
                  </svg>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    className="form-input"
                    placeholder="Email Address"
                    required
                    autoComplete="email"
                  />
                </div>
              </div>

              <div className="form-group">
                <div className="input-wrapper">
                  <svg className="input-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M15 8V5C15 2.23858 12.7614 0 10 0C7.23858 0 5 2.23858 5 5V8H3V20H17V8H15ZM7 5C7 3.34315 8.34315 2 10 2C11.6569 2 13 3.34315 13 5V8H7V5ZM15 10V18H5V10H15Z" fill="#9CA3AF"/>
                  </svg>
                  <input
                    type="password"
                    id="password"
                    name="password"
                    value={formData.password}
                    onChange={handleChange}
                    className="form-input"
                    placeholder="Password"
                    required
                    autoComplete="new-password"
                    readOnly
                    onFocus={(e) => e.target.removeAttribute('readonly')}
                  />
                </div>
              </div>

              <div className="form-group">
                <div className="input-wrapper">
                  <svg className="input-icon" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M4 4H16V16H4V4ZM2 4C2 2.89543 2.89543 2 4 2H16C17.1046 2 18 2.89543 18 4V16C18 17.1046 17.1046 18 16 18H4C2.89543 18 2 17.1046 2 16V4ZM6 6H14V8H6V6ZM6 10H14V12H6V10ZM6 14H10V16H6V14Z" fill="#9CA3AF"/>
                  </svg>
                  <input
                    type="text"
                    id="userCode"
                    name="userCode"
                    value={formData.userCode}
                    onChange={handleChange}
                    className="form-input"
                    placeholder="UserCode"
                    required
                    autoComplete="off"
                    readOnly
                    onFocus={(e) => e.target.removeAttribute('readonly')}
                  />
                </div>
              </div>

              <div className="form-options">
                <label className="checkbox-label">
                  <input
                    type="checkbox"
                    checked={keepLoggedIn}
                    onChange={(e) => setKeepLoggedIn(e.target.checked)}
                    className="checkbox-input"
                  />
                  <span className="checkbox-text">Keep me logged in</span>
                </label>
                <a href="#" className="forgot-password-link">Forgot password?</a>
              </div>

              <button
                type="submit"
                className="login-button"
                disabled={loading}
              >
                {loading ? (
                  <span className="button-loading">
                    <span className="spinner"></span>
                    Logging in...
                  </span>
                ) : (
                  'Log in'
                )}
              </button>

              <div className="support-link-container">
                <a href="#" className="support-link">Contact GME Support</a>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;

