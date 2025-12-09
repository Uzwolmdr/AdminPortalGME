import { useState } from 'react';
import { changePasswordAPI } from '../services/api';
import './ChangePassword.css';

const ChangePassword = () => {
  const [formData, setFormData] = useState({
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
  });
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
      // Get email and userCode from localStorage (set during login)
      const email = localStorage.getItem('userEmail');
      const userCode = localStorage.getItem('userCode');

      if (!email || !userCode) {
        setError('User information not found. Please login again.');
        setLoading(false);
        return;
      }

      const response = await changePasswordAPI(email, userCode, formData.oldPassword, formData.newPassword);

      if (response) {
        const responseCode = response.ResponseCode || response.responseCode;
        const responseDescription = response.ResponseDescription || response.responseDescription;

        if (responseDescription) {
          setResponseMessage(responseDescription);
          setIsSuccess(responseCode === '100' || responseCode === 100);
        }

        if (responseCode === '100' || responseCode === 100) {
          // Clear form on success
          setFormData({
            oldPassword: '',
            newPassword: '',
            confirmPassword: '',
          });
        }
      }
    } catch (err) {
      console.error('Change password error:', err);
      if (err.data) {
        const responseCode = err.data.ResponseCode || err.data.responseCode;
        const responseDescription = err.data.ResponseDescription || err.data.responseDescription;

        if (responseDescription) {
          setResponseMessage(responseDescription);
          setIsSuccess(responseCode === '100' || responseCode === 100);
        } else {
          setError(err.message || 'Failed to change password. Please try again.');
        }
      } else {
        setError(err.message || 'Failed to change password. Please try again.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="change-password-page">
      <div className="change-password-header">
        <div className="page-icon-title">
          <svg className="page-icon" width="24" height="24" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M12 8V4C12 1.79086 10.2091 0 8 0C5.79086 0 4 1.79086 4 4V8H2V16H14V8H12ZM6 4C6 2.89543 6.89543 2 8 2C9.10457 2 10 2.89543 10 4V8H6V4ZM12 10V14H4V10H12Z" fill="#9CA3AF"/>
          </svg>
          <h1 className="page-title">Change Password</h1>
        </div>
      </div>

      <div className="change-password-content">
        <div className="change-password-card">
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

          <form onSubmit={handleSubmit} className="change-password-form">
            <div className="form-group">
              <label htmlFor="oldPassword" className="form-label">
                Old Password
              </label>
              <input
                type="password"
                id="oldPassword"
                name="oldPassword"
                value={formData.oldPassword}
                onChange={handleChange}
                className="form-input"
                placeholder="Enter your old password"
                required
                autoComplete="current-password"
                readOnly
                onFocus={(e) => e.target.removeAttribute('readonly')}
              />
            </div>

            <div className="form-group">
              <label htmlFor="newPassword" className="form-label">
                New Password
              </label>
              <input
                type="password"
                id="newPassword"
                name="newPassword"
                value={formData.newPassword}
                onChange={handleChange}
                className="form-input"
                placeholder="Enter your new password"
                required
                autoComplete="new-password"
                readOnly
                onFocus={(e) => e.target.removeAttribute('readonly')}
              />
            </div>

            <div className="form-group">
              <label htmlFor="confirmPassword" className="form-label">
                Confirm New Password
              </label>
              <input
                type="password"
                id="confirmPassword"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleChange}
                className="form-input"
                placeholder="Confirm your new password"
                required
                autoComplete="new-password"
                readOnly
                onFocus={(e) => e.target.removeAttribute('readonly')}
              />
            </div>

            <button
              type="submit"
              className="change-password-button"
              disabled={loading}
            >
              {loading ? (
                <span className="button-loading">
                  <span className="spinner"></span>
                  Changing password...
                </span>
              ) : (
                'Change Password'
              )}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ChangePassword;

