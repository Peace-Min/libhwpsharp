using HwpLib.Object.DocInfo.BorderFill.FillInfo;
using HwpLib.Object.DocInfo.Numbering;
using HwpLib.Object.Etc;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 글머리표에 대한 레코드
    /// </summary>
    public class Bullet
    {
        private readonly ParagraphHeadInfo _paragraphHeadInfo;
        private readonly HWPString _bulletChar;
        private bool _imageBullet;
        private readonly PictureInfo _imageBulletInfo;
        private readonly HWPString _checkBulletChar;

        public Bullet()
        {
            _paragraphHeadInfo = new ParagraphHeadInfo();
            _bulletChar = new HWPString();
            _imageBullet = false;
            _imageBulletInfo = new PictureInfo();
            _checkBulletChar = new HWPString();
        }

        public ParagraphHeadInfo ParagraphHeadInfo => _paragraphHeadInfo;
        public HWPString BulletChar => _bulletChar;

        public bool ImageBullet
        {
            get => _imageBullet;
            set => _imageBullet = value;
        }

        public PictureInfo ImageBulletInfo => _imageBulletInfo;
        public HWPString CheckBulletChar => _checkBulletChar;

        public Bullet Clone()
        {
            var cloned = new Bullet();
            cloned._paragraphHeadInfo.Copy(_paragraphHeadInfo);
            cloned._bulletChar.Copy(_bulletChar);
            cloned._imageBullet = _imageBullet;
            cloned._imageBulletInfo.Copy(_imageBulletInfo);
            cloned._checkBulletChar.Copy(_checkBulletChar);
            return cloned;
        }
    }

}